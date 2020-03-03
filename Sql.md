 # Merge statement 
 ```
 /* Selecting the Target and the Source */ 
MERGE PRODUCT_LIST AS TARGET 
	USING UPDATE_LIST AS SOURCE 

	/* 1. Performing the UPDATE operation */ 

	/* If the P_ID is same, 
	check for change in P_NAME or P_PRICE */ 
	ON (TARGET.P_ID = SOURCE.P_ID) 
	WHEN MATCHED 
		AND TARGET.P_NAME <> SOURCE.P_NAME 
		OR TARGET.P_PRICE <> SOURCE.P_PRICE 

	/* Update the records in TARGET */ 
	THEN UPDATE
		SET TARGET.P_NAME = SOURCE.P_NAME, 
		TARGET.P_PRICE = SOURCE.P_PRICE 
	
	/* 2. Performing the INSERT operation */ 

	/* When no records are matched with TARGET table
	Then insert the records in the target table */ 
	WHEN NOT MATCHED BY TARGET 
	THEN INSERT (P_ID, P_NAME, P_PRICE)		 
		VALUES (SOURCE.P_ID, SOURCE.P_NAME, SOURCE.P_PRICE) 

	/* 3. Performing the DELETE operation */ 

	/* When no records are matched with SOURCE table
	Then delete the records from the target table */ 
	WHEN NOT MATCHED BY SOURCE 
	THEN DELETE

/* END OF MERGE */ 
```
