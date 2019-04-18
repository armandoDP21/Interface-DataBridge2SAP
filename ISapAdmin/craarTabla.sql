-- ****** Object: Table PRODODS.VSC_EMBARQUES Script Date: 11/23/2010 5:48:39 PM ******
CREATE TABLE "VSC_EMBARQUES" (
  "PAIS_CLAVE" NUMBER(22,0) NOT NULL,
  "SUC_CLAVE" NUMBER(22,0) NOT NULL,
  "MMP_FECHA" DATE NOT NULL,
  "MOV_CLAVE" NUMBER(22,0) NOT NULL,
  "MMP_FOLIO" NUMBER(22,0) NOT NULL,
  "PRO_CLAVE" VARCHAR2(10 BYTE) NOT NULL,
  "PRO_GRAMAJ" NUMBER(22,0) NOT NULL,
  "PAIS_DESCRI" VARCHAR2(50 BYTE) NOT NULL,
  "PRO_DESCRI" VARCHAR2(50 BYTE) NOT NULL,
  "DMP_CANTID" NUMBER(22,0) NOT NULL,
  "DMP_PRECIO" NUMBER(10,0) NOT NULL,
  "SAP_PERIOD" CHAR(3 BYTE) NOT NULL,
  "SAP_NUM_MATERIAL_LEGADO" CHAR(18 BYTE) NOT NULL,
  "SAP_FISCAL_YEAR" NUMBER NOT NULL)
/
