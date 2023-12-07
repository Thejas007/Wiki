```
SELECT
    a.table_name,
    c.column_name,
    b.table_name AS child_table,
    d.column_name,
    b.r_constraint_name
FROM
    user_constraints  a,
    user_constraints  b,
    user_ind_columns  c,
    user_cons_columns d
WHERE
        a.constraint_type = 'P'
    AND a.constraint_name = b.r_constraint_name
    AND b.constraint_type = 'R'
    AND a.table_name = c.table_name
    AND a.constraint_name = c.index_name
    AND b.constraint_name = d.constraint_name
    AND a.table_name = 'SYSTEMCODEGROUP' ;
```
