PGDMP     9    %                z            baza_podataka    13.5    13.5 G    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                        0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16394    baza_podataka    DATABASE     l   CREATE DATABASE baza_podataka WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Croatian_Croatia.1250';
    DROP DATABASE baza_podataka;
                postgres    false            �            1255    24764    azuriranje_izvjesca()    FUNCTION     �  CREATE FUNCTION public.azuriranje_izvjesca() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
declare
slog record;
najveci_broj int = 0;
datum_dnevnika date;
kursor_pr cursor for select * from dnevnik where dnevnik.fk_trudnica = new.fk_trudnica and dnevnik.datum >= new.datum_pocetka_vodenja and
dnevnik.datum <= new.datum_kraja_dnevnika;
begin
		for slog in kursor_pr loop
        if slog.broj_puta_povracanja > najveci_broj then
            najveci_broj = slog.broj_puta_povracanja;
			datum_dnevnika = slog.datum;
        end if;
		end loop;
		update public.izvjesce set datum_kada_najvise_povracala = datum_dnevnika;
		return new;
end
$$;
 ,   DROP FUNCTION public.azuriranje_izvjesca();
       public          postgres    false            �            1255    16756    odredivanje_termina_poroda()    FUNCTION     �   CREATE FUNCTION public.odredivanje_termina_poroda() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
	update trudnica set termin_poroda = zadnja_menstruacija + integer '280';
	return new;
end
$$;
 3   DROP FUNCTION public.odredivanje_termina_poroda();
       public          postgres    false            �            1255    16772    provjera_datuma_rodenja()    FUNCTION     �  CREATE FUNCTION public.provjera_datuma_rodenja() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
	declare 
	danasnji_datum date;
	broj_godina integer;
begin
	select current_date into danasnji_datum;
	select extract(years from age(new.datum_rodenja::date, danasnji_datum::date)) into broj_godina;
	if broj_godina >= '-8' or broj_godina <= '-60' then raise exception 'Nije moguće da trudnica ima više od 60 godina ili manje od 8.';
	else return new;
	end if;
end
$$;
 0   DROP FUNCTION public.provjera_datuma_rodenja();
       public          postgres    false            �            1255    24777    provjera_datuma_unosa_dnevnik()    FUNCTION     �  CREATE FUNCTION public.provjera_datuma_unosa_dnevnik() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
declare
postoji boolean = false;
kursor cursor for select * from dnevnik where fk_trudnica = new.fk_trudnica;
begin
        for slog in kursor loop
        if(new.datum = slog.datum) then postoji = true;
        end if;
        end loop;
		if (postoji = false) then return new;
		else raise exception 'Već postoji zapis u dnevniku za današnji datum.';
		end if;
end
$$;
 6   DROP FUNCTION public.provjera_datuma_unosa_dnevnik();
       public          postgres    false            �            1255    24691    provjera_emaila()    FUNCTION     �   CREATE FUNCTION public.provjera_emaila() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
	if new.email like '%_@__%.__%' then return new;
	else raise exception 'Email adresa nije ispravnog oblika.';
	end if;
end
$$;
 (   DROP FUNCTION public.provjera_emaila();
       public          postgres    false            �            1255    16758    provjera_ispravnosti_oib()    FUNCTION     �   CREATE FUNCTION public.provjera_ispravnosti_oib() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
	if(length(new.OIB) != 11) then
	raise exception 'OIB treba sadržavati 11 znakova, a ne %.', length(new.OIB);
	end if;
	return new;
end;
$$;
 1   DROP FUNCTION public.provjera_ispravnosti_oib();
       public          postgres    false            �            1255    24762    provjera_izvjesca()    FUNCTION     �  CREATE FUNCTION public.provjera_izvjesca() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
declare
slog record;
najveci_broj int = 0;
danasnji_datum date;
datum_dnevnika date;
kursor_pr cursor for select * from dnevnik where dnevnik.fk_trudnica = new.fk_trudnica and dnevnik.datum >= new.datum_pocetka_vodenja and
dnevnik.datum <= new.datum_kraja_dnevnika;
begin
		select current_date into danasnji_datum;
		for slog in kursor_pr loop
        if slog.broj_puta_povracanja > najveci_broj then
            najveci_broj = slog.broj_puta_povracanja;
			datum_dnevnika = slog.datum;
        end if;
		end loop;
		if (new.datum_pocetka_vodenja >= new.datum_kraja_dnevnika) then
			raise exception 'Kreiranje izvješća nije moguće jer je datum početka veći ili jednak datumu kraja.';
		elsif (new.datum_pocetka_vodenja >= danasnji_datum ) then
			raise exception 'Kreiranje izvješća nije moguće jer je datum početka isti ili veći od današnjeg dana.';
		elsif (new.datum_kraja_dnevnika >= danasnji_datum) then
			raise exception 'Kreiranje izvješća nije moguće jer je datum kraja isti ili veći od današnjeg dana';
		elseif (najveci_broj = 0) then
			raise exception 'U tom razdoblju ne postoji zapis kada je trudnica povraćala.';
		end if;
		return new;
end
$$;
 *   DROP FUNCTION public.provjera_izvjesca();
       public          postgres    false            �            1255    24693    provjera_trajanja_ciklusa()    FUNCTION       CREATE FUNCTION public.provjera_trajanja_ciklusa() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
	if new.trajanje_ciklusa between '20' and '40' then return new;
	else raise exception 'Ciklus ne može trajati manje od 20 dana ili više od 40 dana.';
	end if;
end
$$;
 2   DROP FUNCTION public.provjera_trajanja_ciklusa();
       public          postgres    false            �            1255    24659    provjera_vrijednosti_kks()    FUNCTION     �  CREATE FUNCTION public.provjera_vrijednosti_kks() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
declare 
trudnica integer;
datum_nove_kontrole date;
begin
    select kontrola.fk_trudnica into trudnica from kontrola where new.fk_kontrola = kontrola.id_kontrola;
	datum_nove_kontrole = new.datum_nalaza + integer '5';
	if (new.KKS = 'loš nalaz') then insert into kontrola (datum_pregleda, fk_trudnica) VALUES ( datum_nove_kontrole, trudnica);
	end if;
	return new;
end
$$;
 1   DROP FUNCTION public.provjera_vrijednosti_kks();
       public          postgres    false            �            1255    16852     provjeri_dan_sljedece_kontrole()    FUNCTION     }  CREATE FUNCTION public.provjeri_dan_sljedece_kontrole() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
declare
zadnji_uneseni_redak kontrola%rowtype;
begin
	select * into zadnji_uneseni_redak from kontrola order by id_kontrola desc limit 1;
	if extract(dow from zadnji_uneseni_redak.datum_pregleda) = 0 then update kontrola 
	set datum_pregleda = datum_pregleda + interval '1 day';
	elsif extract(dow from zadnji_uneseni_redak.datum_pregleda) = 6 then update kontrola 
	set datum_pregleda = datum_pregleda + interval '2 day'
	where kontrola.id_kontrola = zadnji_uneseni_redak.id_kontrola;
	else return new;
	end if;
	return new;
end
$$;
 7   DROP FUNCTION public.provjeri_dan_sljedece_kontrole();
       public          postgres    false            �            1259    24621    dnevnik    TABLE     g  CREATE TABLE public.dnevnik (
    id_dnevnik integer NOT NULL,
    mucnina character varying,
    broj_puta_povracanja integer,
    slabost character varying,
    opis_prehrane character varying(100),
    bol_u_zglobovima character varying,
    ostali_simptomi character varying(50),
    datum date,
    zgaravica character varying,
    fk_trudnica bigint
);
    DROP TABLE public.dnevnik;
       public         heap    postgres    false            �            1259    24619    dnevnik_id_dnevnik_seq    SEQUENCE     �   CREATE SEQUENCE public.dnevnik_id_dnevnik_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.dnevnik_id_dnevnik_seq;
       public          postgres    false    211                       0    0    dnevnik_id_dnevnik_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.dnevnik_id_dnevnik_seq OWNED BY public.dnevnik.id_dnevnik;
          public          postgres    false    210            �            1259    24608    izvjesce    TABLE     �   CREATE TABLE public.izvjesce (
    id_izvjesce integer NOT NULL,
    datum_pocetka_vodenja date,
    datum_kraja_dnevnika date,
    datum_kada_najvise_povracala date,
    fk_trudnica integer
);
    DROP TABLE public.izvjesce;
       public         heap    postgres    false            �            1259    24606    izvjesce_id_izvjesce_seq    SEQUENCE     �   CREATE SEQUENCE public.izvjesce_id_izvjesce_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.izvjesce_id_izvjesce_seq;
       public          postgres    false    209                       0    0    izvjesce_id_izvjesce_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.izvjesce_id_izvjesce_seq OWNED BY public.izvjesce.id_izvjesce;
          public          postgres    false    208            �            1259    16655    kontrola    TABLE       CREATE TABLE public.kontrola (
    id_kontrola integer NOT NULL,
    datum_pregleda date NOT NULL,
    uzv text,
    tt integer,
    sistolicki_tlak integer,
    dijastolicki_tlak integer,
    kcs integer,
    ostali_pregledi text,
    fk_trudnica integer
);
    DROP TABLE public.kontrola;
       public         heap    postgres    false            �            1259    16653    kontrola_id_kontrola_seq    SEQUENCE     �   CREATE SEQUENCE public.kontrola_id_kontrola_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.kontrola_id_kontrola_seq;
       public          postgres    false    205                       0    0    kontrola_id_kontrola_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.kontrola_id_kontrola_seq OWNED BY public.kontrola.id_kontrola;
          public          postgres    false    204            �            1259    16745    laboratorijski_nalazi    TABLE       CREATE TABLE public.laboratorijski_nalazi (
    id_laboratorijski_nalaz integer NOT NULL,
    kks character varying(50) NOT NULL,
    urin_sed character varying(50) NOT NULL,
    guk character varying(50) NOT NULL,
    datum_nalaza date NOT NULL,
    fk_kontrola integer
);
 )   DROP TABLE public.laboratorijski_nalazi;
       public         heap    postgres    false            �            1259    16743 1   laboratorijski_nalazi_id_laboratorijski_nalaz_seq    SEQUENCE     �   CREATE SEQUENCE public.laboratorijski_nalazi_id_laboratorijski_nalaz_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 H   DROP SEQUENCE public.laboratorijski_nalazi_id_laboratorijski_nalaz_seq;
       public          postgres    false    207                       0    0 1   laboratorijski_nalazi_id_laboratorijski_nalaz_seq    SEQUENCE OWNED BY     �   ALTER SEQUENCE public.laboratorijski_nalazi_id_laboratorijski_nalaz_seq OWNED BY public.laboratorijski_nalazi.id_laboratorijski_nalaz;
          public          postgres    false    206            �            1259    16613    lijecnik    TABLE     �   CREATE TABLE public.lijecnik (
    id_lijecnik integer NOT NULL,
    ime character varying(25) NOT NULL,
    prezime character varying(25) NOT NULL,
    broj_telefona character varying(15) NOT NULL,
    adresa character varying(50) NOT NULL
);
    DROP TABLE public.lijecnik;
       public         heap    postgres    false            �            1259    16611    lijecnik_id_lijecnik_seq    SEQUENCE     �   CREATE SEQUENCE public.lijecnik_id_lijecnik_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.lijecnik_id_lijecnik_seq;
       public          postgres    false    203                       0    0    lijecnik_id_lijecnik_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.lijecnik_id_lijecnik_seq OWNED BY public.lijecnik.id_lijecnik;
          public          postgres    false    202            �            1259    16599    trudnica    TABLE     f  CREATE TABLE public.trudnica (
    id_trudnica integer NOT NULL,
    oib character varying(11) NOT NULL,
    ime character varying(25) NOT NULL,
    prezime character varying(25) NOT NULL,
    datum_rodenja date NOT NULL,
    adresa character varying(50) NOT NULL,
    broj_mobitela character varying(15) NOT NULL,
    zadnja_menstruacija date NOT NULL,
    termin_poroda date,
    spol_djeteta character(1),
    fk_lijecnik bigint,
    email character varying(25),
    trajanje_ciklusa smallint,
    CONSTRAINT trudnica_spol_djeteta_check CHECK (((spol_djeteta = 'M'::bpchar) OR (spol_djeteta = 'Z'::bpchar)))
);
    DROP TABLE public.trudnica;
       public         heap    postgres    false            �            1259    16597    trudnica_id_trudnica_seq    SEQUENCE     �   CREATE SEQUENCE public.trudnica_id_trudnica_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.trudnica_id_trudnica_seq;
       public          postgres    false    201                       0    0    trudnica_id_trudnica_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.trudnica_id_trudnica_seq OWNED BY public.trudnica.id_trudnica;
          public          postgres    false    200            R           2604    24624    dnevnik id_dnevnik    DEFAULT     x   ALTER TABLE ONLY public.dnevnik ALTER COLUMN id_dnevnik SET DEFAULT nextval('public.dnevnik_id_dnevnik_seq'::regclass);
 A   ALTER TABLE public.dnevnik ALTER COLUMN id_dnevnik DROP DEFAULT;
       public          postgres    false    211    210    211            Q           2604    24611    izvjesce id_izvjesce    DEFAULT     |   ALTER TABLE ONLY public.izvjesce ALTER COLUMN id_izvjesce SET DEFAULT nextval('public.izvjesce_id_izvjesce_seq'::regclass);
 C   ALTER TABLE public.izvjesce ALTER COLUMN id_izvjesce DROP DEFAULT;
       public          postgres    false    209    208    209            O           2604    16658    kontrola id_kontrola    DEFAULT     |   ALTER TABLE ONLY public.kontrola ALTER COLUMN id_kontrola SET DEFAULT nextval('public.kontrola_id_kontrola_seq'::regclass);
 C   ALTER TABLE public.kontrola ALTER COLUMN id_kontrola DROP DEFAULT;
       public          postgres    false    205    204    205            P           2604    16748 -   laboratorijski_nalazi id_laboratorijski_nalaz    DEFAULT     �   ALTER TABLE ONLY public.laboratorijski_nalazi ALTER COLUMN id_laboratorijski_nalaz SET DEFAULT nextval('public.laboratorijski_nalazi_id_laboratorijski_nalaz_seq'::regclass);
 \   ALTER TABLE public.laboratorijski_nalazi ALTER COLUMN id_laboratorijski_nalaz DROP DEFAULT;
       public          postgres    false    207    206    207            N           2604    16616    lijecnik id_lijecnik    DEFAULT     |   ALTER TABLE ONLY public.lijecnik ALTER COLUMN id_lijecnik SET DEFAULT nextval('public.lijecnik_id_lijecnik_seq'::regclass);
 C   ALTER TABLE public.lijecnik ALTER COLUMN id_lijecnik DROP DEFAULT;
       public          postgres    false    203    202    203            L           2604    16602    trudnica id_trudnica    DEFAULT     |   ALTER TABLE ONLY public.trudnica ALTER COLUMN id_trudnica SET DEFAULT nextval('public.trudnica_id_trudnica_seq'::regclass);
 C   ALTER TABLE public.trudnica ALTER COLUMN id_trudnica DROP DEFAULT;
       public          postgres    false    200    201    201            �          0    24621    dnevnik 
   TABLE DATA           �   COPY public.dnevnik (id_dnevnik, mucnina, broj_puta_povracanja, slabost, opis_prehrane, bol_u_zglobovima, ostali_simptomi, datum, zgaravica, fk_trudnica) FROM stdin;
    public          postgres    false    211   k       �          0    24608    izvjesce 
   TABLE DATA           �   COPY public.izvjesce (id_izvjesce, datum_pocetka_vodenja, datum_kraja_dnevnika, datum_kada_najvise_povracala, fk_trudnica) FROM stdin;
    public          postgres    false    209   �k       �          0    16655    kontrola 
   TABLE DATA           �   COPY public.kontrola (id_kontrola, datum_pregleda, uzv, tt, sistolicki_tlak, dijastolicki_tlak, kcs, ostali_pregledi, fk_trudnica) FROM stdin;
    public          postgres    false    205   �k       �          0    16745    laboratorijski_nalazi 
   TABLE DATA           w   COPY public.laboratorijski_nalazi (id_laboratorijski_nalaz, kks, urin_sed, guk, datum_nalaza, fk_kontrola) FROM stdin;
    public          postgres    false    207   fl       �          0    16613    lijecnik 
   TABLE DATA           T   COPY public.lijecnik (id_lijecnik, ime, prezime, broj_telefona, adresa) FROM stdin;
    public          postgres    false    203   �l       �          0    16599    trudnica 
   TABLE DATA           �   COPY public.trudnica (id_trudnica, oib, ime, prezime, datum_rodenja, adresa, broj_mobitela, zadnja_menstruacija, termin_poroda, spol_djeteta, fk_lijecnik, email, trajanje_ciklusa) FROM stdin;
    public          postgres    false    201   m                  0    0    dnevnik_id_dnevnik_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.dnevnik_id_dnevnik_seq', 89, true);
          public          postgres    false    210            	           0    0    izvjesce_id_izvjesce_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.izvjesce_id_izvjesce_seq', 1955, true);
          public          postgres    false    208            
           0    0    kontrola_id_kontrola_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.kontrola_id_kontrola_seq', 43, true);
          public          postgres    false    204                       0    0 1   laboratorijski_nalazi_id_laboratorijski_nalaz_seq    SEQUENCE SET     `   SELECT pg_catalog.setval('public.laboratorijski_nalazi_id_laboratorijski_nalaz_seq', 49, true);
          public          postgres    false    206                       0    0    lijecnik_id_lijecnik_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.lijecnik_id_lijecnik_seq', 2, true);
          public          postgres    false    202                       0    0    trudnica_id_trudnica_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.trudnica_id_trudnica_seq', 51, true);
          public          postgres    false    200            ^           2606    24626    dnevnik dnevnik_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.dnevnik
    ADD CONSTRAINT dnevnik_pkey PRIMARY KEY (id_dnevnik);
 >   ALTER TABLE ONLY public.dnevnik DROP CONSTRAINT dnevnik_pkey;
       public            postgres    false    211            \           2606    24613    izvjesce izvjesce_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.izvjesce
    ADD CONSTRAINT izvjesce_pkey PRIMARY KEY (id_izvjesce);
 @   ALTER TABLE ONLY public.izvjesce DROP CONSTRAINT izvjesce_pkey;
       public            postgres    false    209            X           2606    16663    kontrola kontrola_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.kontrola
    ADD CONSTRAINT kontrola_pkey PRIMARY KEY (id_kontrola);
 @   ALTER TABLE ONLY public.kontrola DROP CONSTRAINT kontrola_pkey;
       public            postgres    false    205            Z           2606    16750 0   laboratorijski_nalazi laboratorijski_nalazi_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.laboratorijski_nalazi
    ADD CONSTRAINT laboratorijski_nalazi_pkey PRIMARY KEY (id_laboratorijski_nalaz);
 Z   ALTER TABLE ONLY public.laboratorijski_nalazi DROP CONSTRAINT laboratorijski_nalazi_pkey;
       public            postgres    false    207            V           2606    16618    lijecnik lijecnik_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.lijecnik
    ADD CONSTRAINT lijecnik_pkey PRIMARY KEY (id_lijecnik);
 @   ALTER TABLE ONLY public.lijecnik DROP CONSTRAINT lijecnik_pkey;
       public            postgres    false    203            T           2606    16605    trudnica trudnica_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.trudnica
    ADD CONSTRAINT trudnica_pkey PRIMARY KEY (id_trudnica);
 @   ALTER TABLE ONLY public.trudnica DROP CONSTRAINT trudnica_pkey;
       public            postgres    false    201            k           2620    24765    izvjesce azuriraj_izvjesce    TRIGGER     }   CREATE TRIGGER azuriraj_izvjesce AFTER INSERT ON public.izvjesce FOR EACH ROW EXECUTE FUNCTION public.azuriranje_izvjesca();
 3   DROP TRIGGER azuriraj_izvjesce ON public.izvjesce;
       public          postgres    false    231    209            d           2620    16757    trudnica odredi_termin_poroda    TRIGGER     �   CREATE TRIGGER odredi_termin_poroda AFTER INSERT ON public.trudnica FOR EACH ROW EXECUTE FUNCTION public.odredivanje_termina_poroda();
 6   DROP TRIGGER odredi_termin_poroda ON public.trudnica;
       public          postgres    false    201    224            e           2620    16759    trudnica provjera_duljine_oib    TRIGGER     �   CREATE TRIGGER provjera_duljine_oib BEFORE INSERT OR UPDATE ON public.trudnica FOR EACH ROW EXECUTE FUNCTION public.provjera_ispravnosti_oib();
 6   DROP TRIGGER provjera_duljine_oib ON public.trudnica;
       public          postgres    false    201    212            h           2620    24694    trudnica provjeri_ciklus    TRIGGER     �   CREATE TRIGGER provjeri_ciklus BEFORE INSERT OR UPDATE ON public.trudnica FOR EACH ROW EXECUTE FUNCTION public.provjera_trajanja_ciklusa();
 1   DROP TRIGGER provjeri_ciklus ON public.trudnica;
       public          postgres    false    201    228            i           2620    16853    kontrola provjeri_dan_kontrole    TRIGGER     �   CREATE TRIGGER provjeri_dan_kontrole AFTER INSERT OR UPDATE ON public.kontrola FOR EACH ROW EXECUTE FUNCTION public.provjeri_dan_sljedece_kontrole();
 7   DROP TRIGGER provjeri_dan_kontrole ON public.kontrola;
       public          postgres    false    205    225            m           2620    24778    dnevnik provjeri_datum    TRIGGER     �   CREATE TRIGGER provjeri_datum BEFORE INSERT ON public.dnevnik FOR EACH ROW EXECUTE FUNCTION public.provjera_datuma_unosa_dnevnik();
 /   DROP TRIGGER provjeri_datum ON public.dnevnik;
       public          postgres    false    211    232            f           2620    16773    trudnica provjeri_datum_rodenja    TRIGGER     �   CREATE TRIGGER provjeri_datum_rodenja BEFORE INSERT OR UPDATE ON public.trudnica FOR EACH ROW EXECUTE FUNCTION public.provjera_datuma_rodenja();
 8   DROP TRIGGER provjeri_datum_rodenja ON public.trudnica;
       public          postgres    false    201    226            g           2620    24692    trudnica provjeri_email    TRIGGER     �   CREATE TRIGGER provjeri_email BEFORE INSERT OR UPDATE ON public.trudnica FOR EACH ROW EXECUTE FUNCTION public.provjera_emaila();
 0   DROP TRIGGER provjeri_email ON public.trudnica;
       public          postgres    false    201    227            l           2620    24763    izvjesce provjeri_izvjesce    TRIGGER     |   CREATE TRIGGER provjeri_izvjesce BEFORE INSERT ON public.izvjesce FOR EACH ROW EXECUTE FUNCTION public.provjera_izvjesca();
 3   DROP TRIGGER provjeri_izvjesce ON public.izvjesce;
       public          postgres    false    209    230            j           2620    24660 .   laboratorijski_nalazi provjeri_vrijednosti_kks    TRIGGER     �   CREATE TRIGGER provjeri_vrijednosti_kks AFTER INSERT ON public.laboratorijski_nalazi FOR EACH ROW EXECUTE FUNCTION public.provjera_vrijednosti_kks();
 G   DROP TRIGGER provjeri_vrijednosti_kks ON public.laboratorijski_nalazi;
       public          postgres    false    207    229            _           2606    24673    trudnica fk_lijecnik    FK CONSTRAINT     �   ALTER TABLE ONLY public.trudnica
    ADD CONSTRAINT fk_lijecnik FOREIGN KEY (fk_lijecnik) REFERENCES public.lijecnik(id_lijecnik) NOT VALID;
 >   ALTER TABLE ONLY public.trudnica DROP CONSTRAINT fk_lijecnik;
       public          postgres    false    2902    201    203            c           2606    24695    dnevnik fk_trudnica    FK CONSTRAINT     �   ALTER TABLE ONLY public.dnevnik
    ADD CONSTRAINT fk_trudnica FOREIGN KEY (fk_trudnica) REFERENCES public.trudnica(id_trudnica) NOT VALID;
 =   ALTER TABLE ONLY public.dnevnik DROP CONSTRAINT fk_trudnica;
       public          postgres    false    201    2900    211            b           2606    24614 "   izvjesce izvjesce_fk_trudnica_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.izvjesce
    ADD CONSTRAINT izvjesce_fk_trudnica_fkey FOREIGN KEY (fk_trudnica) REFERENCES public.trudnica(id_trudnica) ON UPDATE CASCADE ON DELETE CASCADE;
 L   ALTER TABLE ONLY public.izvjesce DROP CONSTRAINT izvjesce_fk_trudnica_fkey;
       public          postgres    false    2900    201    209            `           2606    16669 "   kontrola kontrola_fk_trudnica_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.kontrola
    ADD CONSTRAINT kontrola_fk_trudnica_fkey FOREIGN KEY (fk_trudnica) REFERENCES public.trudnica(id_trudnica) ON UPDATE CASCADE ON DELETE CASCADE;
 L   ALTER TABLE ONLY public.kontrola DROP CONSTRAINT kontrola_fk_trudnica_fkey;
       public          postgres    false    201    205    2900            a           2606    16751 <   laboratorijski_nalazi laboratorijski_nalazi_fk_kontrola_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.laboratorijski_nalazi
    ADD CONSTRAINT laboratorijski_nalazi_fk_kontrola_fkey FOREIGN KEY (fk_kontrola) REFERENCES public.kontrola(id_kontrola) ON UPDATE CASCADE ON DELETE CASCADE;
 f   ALTER TABLE ONLY public.laboratorijski_nalazi DROP CONSTRAINT laboratorijski_nalazi_fk_kontrola_fkey;
       public          postgres    false    207    205    2904            �   �   x�37�tq�4����:
E�G�%��y����FFF����F !C.0�S��Q��������������-+*�O�I,�LF�l���e&%�e���Ԝ�TN?WΒ�����$$�� a�N�&�N�������9�����qqq ��9s      �   $   x�3�45�4202�50"(�ࢆƜ�\1z\\\ ���      �   i   x�]���0C��,P��M)K0����@B�o��I���1m�#
C�ˆu^�Pҏl�w��H|	>~d|˒��KVAH_P��`	��D��S��&��m       �   C   x�3����?�P!/1'�
��44�4202�50�56�41�2��L�OJ,�*�9�ઌL9��b���� }v      �   :   x�3������dNK]K�H-)JT�.J-�����Mt�b���� ���      �   �   x�M�1�0Eg�\���4��`d�X�(�)��b�N�������2��!��If{Z;��VUdA�p[�b'�tB�[AHɓ�/32��g��Q��qyU?1\7�II=]Ґ��:�->=v���6��#�4�{!�ĝ1.     