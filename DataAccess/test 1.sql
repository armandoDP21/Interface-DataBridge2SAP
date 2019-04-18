-- ****** Object: Stored Procedure PRODODS.DBP_AJUSTE_AGRUPA_HAS Script Date: 10/7/2010 12:15:19 PM ******
CREATE PROCEDURE dbp_ajuste_Agrupa_Has (ld_Fecha_Inicial DATE,
                                              ld_fecha_Final   DATE) IS






     CURSOR c_AGRUPACION IS
	select car.nombre as Categoria  , tam.nombre as Tamanio, pro.pro_clave as producto,
               car.carton as clave_cat, tam.carton as clave_tam
	from   bno_producto@SANPEDROSULA pro ,
               bno_carton@SANPEDROSULA   car ,
               bno_carton@SANPEDROSULA   tam
	where  car.carton = pro.categoria
	and    car.tipo =2
	and    tam.carton = pro.tamanio
	and    tam.tipo = 3;



BEGIN

   dbms_output.put_line('inicio de proceso');

    FOR AGR IN C_agrupacion LOOP
	BEGIN
		UPDATE ODS_VTS_PRODUCTO
		SET    PRO_CATEGORIA_HAS_CLAVE = AGR.CLAVE_CAT,
                       PRO_CATEGORIA_HAS_DESCRI= AGR.CATEGORIA,
		       PRO_TAMANO_HAS_CLAVE    = AGR.CLAVE_TAM,
                       PRO_TAMANO_HAS_DESCRI   = AGR.TAMANIO
		WHERE  PRO_CLAVELOCAL          = AGR.PRODUCTO
                AND    CFE_FECHA              >= ld_Fecha_Inicial
                AND    CFE_FECHA              <= ld_fecha_Final
                AND    PAIS_DESCRIPCION='Honduras';

		COMMIT;

	  EXCEPTION
            WHEN NO_DATA_FOUND THEN
		NULL;

	END;
    END LOOP;


   DBMS_OUTPUT.PUT_LINE('Fin de Proceso');
END;
/
