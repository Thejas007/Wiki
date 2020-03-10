Consistency:

Take one example:

SQL> Create table emp1 as select from emp;
SQL> Var x refcursor
Begin
Open :x for select * from emp1;
End;
/

Delete from emp1; Commit:
Now we have no record in empl table.

But if we fetch from ref cursor we will see all the data before the delete i.e. the point of time the query or the cursor were open. 
Below I am showing one pl/sql block to demonstrate the multi-versioning concept. 


SQL> select * from emp1;
NO rows selected.

SQL> Set serverout on
SQL> 
declare
emp_rec emp1%rowtype;
begin
loop
fetch :x into emp_rec;
exit when :x%notfound;
dbms_output.put_line('Empno is:'||emp_rec.empno);
end loop;
EXception
When others then
Dbms_output.put._1ine('Error is:'I Isqlerrm)
End;
/

Empno is:7369
Empno is:7499
Empno is:7521
Empno is:7566
Empno is:7654
Empno is:7698
Empno is:7782
Empno is:7788
Empno is:7839
Empno is:784
Empno is:7876
Empno is:7900
Empno is:7902
Empno is:7934
PL/SQL procedure successfully completed.

How it happen?

When we give delete command, then the original data is put in rollback segment. And other entry 
(i.e., result of dml command) goes to redo log which helps oracle to roll forward the transaction.

So when we run the above pl/sql block oracle reads from rollback segment. If the data block 
header tells the record is changed after the query is fired and hence we see all the emp records as 
output despite no record is there in the table. This concept is known as multi-versioning because
table has 2 set of data i.e, modified data and unmodified data.

In short multi-version allow "read never blocked by write" and "write not blocked by read".


DB installation :

https://oracle-base.com/articles/11g/oracle-db-11gr2-installation-on-oracle-linux-7




# keep dense_rank

http://sql.standout-dev.com/2018/09/the-first-and-last-functions-in-oracle-sql/
```
SELECT 
  MIN(sal) KEEP (DENSE_RANK FIRST ORDER BY sal) AS min_sal_first_sal,
 
  deptno
FROM (
    SELECT 'a' as name, 1 as sal, 1 as deptno FROM DUAL
UNION ALL SELECT 'b', 1, 1 FROM DUAL
UNION ALL SELECT 'c', 1, 1 FROM DUAL
UNION ALL SELECT 'd', 2, 1 FROM DUAL
UNION ALL SELECT 'e', 3, 1 FROM DUAL
UNION ALL SELECT 'f', 3, 1 FROM DUAL
UNION ALL SELECT 'g', 4, 2 FROM DUAL
UNION ALL SELECT 'h', 4, 2 FROM DUAL
UNION ALL SELECT 'i', 5, 2 FROM DUAL
UNION ALL SELECT 'j', 5, 2 FROM DUAL)
group by deptno;
```

# Trasction scope
Commiting and rollbacking the transcation from c# .NET

https://docs.microsoft.com/en-us/dotnet/api/system.transactions.transactionscope.complete?view=netframework-4.8

```
using (var transactionScope = new TransactionScope())
                {
                    using (var cmd = database.GetStoredProcCommand(_storedProcedureName))
                    {
                    database.AddInParameter(cmd, "PARAM1", DbType.Int32, 123);
                    database.AddOutParameter(cmd, OUT_RETURN_CODE_PARAM, DbType.Int32, 1);
                      database.ExecuteNonQuery(cmd);
                    }
                    
                     transactionScope.Complete();
                    
                 }
```
