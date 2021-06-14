
# Execute a store proc
	  variable x refcursor;
	     variable  o_display_msg VARCHAR2;
	    variable o_return_code  NUMBER;
	    exec  PKG_API.GetAccount_ByExtAccountCode(4,'vlt',null,:x,:o_display_msg,:o_return_code);
	print x;
  
# Looping a cursor
	DECLARE
	   c1   SYS_REFCURSOR;
	   l    license%rowtype;
	BEGIN
	   OPEN c1 FOR SELECT  * FROM  license;
	   LOOP
	       FETCH c1 INTO l;
	       EXIT WHEN c1%notfound;
	       dbms_output.put_line(l.gemidentifier);
	   END LOOP;
	END;
    
