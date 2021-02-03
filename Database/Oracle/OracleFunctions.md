1.  ### TRUNC
  The Oracle/PLSQL TRUNC function returns a date truncated to a specific unit of measure.
  ```
  TRUNC ( date [, format ] )
  ```
  format
  Optional. The unit of measure to apply for truncating. If the format parameter is omitted, the TRUNC function will truncate the date to the day value, so that any hours, minutes, or seconds will be truncated off. It can be one of the following values:
  ```
  Year	SYYYY, YYYY, YEAR, SYEAR, YYY, YY, Y
  ISO Year	IYYY, IY, I
  Quarter	Q
  Month	MONTH, MON, MM, RM
  Week	WW
  IW	IW
  W	W
  Day	DDD, DD, J
  Start day of the week	DAY, DY, D
  Hour	HH, HH12, HH24
  Minute	MI
  ```
  Example :
  ```
  TRUNC(TO_DATE('22-AUG-03'), 'YEAR')
  Result: '01-JAN-03'

  TRUNC(TO_DATE('22-AUG-03'), 'Q')--Quarter
  Result: '01-JUL-03'

  TRUNC(TO_DATE('22-AUG-03'), 'MONTH')
  Result: '01-AUG-03'

  TRUNC(TO_DATE('22-AUG-03'), 'DDD')--DAY That is the default
  Result: '22-AUG-03'

  TRUNC(TO_DATE('22-AUG-03'), 'DAY')--Week
  Result: '17-AUG-03'
  ```
