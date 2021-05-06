MERGE INTO intelligencetenantmapping des
        using (select 5 as intelligencetenantid,'c0398c00-9691-48ee-a052-d803afbc6a0e' as tenantidentifier from dual) src
        on (des.intelligencetenantid= src.intelligencetenantid and
            des.tenantidentifier= src.tenantidentifier)
        WHEN NOT matched THEN
        INSERT   (
            intelligencetenantid,
            tenantIdentifier
        ) VALUES (
            5,
            'c0398c00-9691-48ee-a052-d803afbc6a0e'
        );
        
INSERT  into intelligencetenantmapping(
            intelligencetenantid,
            tenantIdentifier
        )
        select             5,            'c0398c00-9691-48ee-a052-d803afbc6a0e' from dual
        where not exists(select * from intelligencetenantmapping where intelligencetenantid=5 and tenantIdentifier='c0398c00-9691-48ee-a052-d803afbc6a0e');
        
