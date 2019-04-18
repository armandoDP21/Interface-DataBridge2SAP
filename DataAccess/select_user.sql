-- ****** Object: Stored Procedure PRODODS.VS_USER_SELECT Script Date: 10/7/2010 11:23:52 AM ******
CREATE PROCEDURE VS_User_Select () IS


BEGIN

SELECT     A.NOMBRE, A.PAIS_CLAVE, A.SUC_CLAVE, B.MONEDA, B.DESCRIPCION, B.MONEDA_CLAVE, A.LAST_LOGIN
FROM         DIC_USUARIOS A INNER JOIN
                      DIC_MONEDAS B ON A.PAIS_CLAVE = B.PAIS_CLAVE
END;
/
