# Automapper
 1 . MemberList.None in mapping
   - When you specify MemberList.None in mapping, Make sure to add all explicit mapping to properities. Otherwise when you  remove any property in source , it get siliently igonered durning mapping.
   - Add unit for all properties mapping.
   
 2 . Default mapping validation by auompper 
  Durning any type change, to get a build error -
   - It is good to a have Mapper.AssertConfigurationIsValid() called in tests; It throws a unit test failure.
   - Or an unit-test to test the mapping. .Map method internally validates the configuraton
  
  
