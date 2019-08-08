 # Javascript promise
        var id = $('#Id').val();
            var data = {
                'id': id
            };
            var serviceCall=function(){
            // Create a new Deferred object
            return new Promise(function(resolve, reject) {
                var options = {
                    type: 'POST',
                    url: '@Url.Action("Action", "Controller")',
                    data: data,
                    success: resolve,
                    error: reject
                };

                $.ajax(options);
            });
            // Return the Deferred's Promise object
            }
            
            var successCallBack=function(result){
            if(result){
            // do the operation
            }
            };
            
            var errorCallBack=function(result, status, xhr, form){
            //Perform error handling
            };
            
            serviceCall().then(successCallBack, errorCallBack);

# Internal working of Promise 
             function Promise(executionFunction) {
               var state = ‘pending’; 
               var deferred = null; 
               var value;
               var resolveCallBacks=[];
                var rejectCallBacks=[];
               function resolve(value){
               state = ‘resolved’; 
                this.resolveCallBacks.forEach((nextFunction) => {
                      storedValue = nextFunction(value);
                   });

               }
               function reject(error){
                state = ‘rejected’; 
              this.rejectCallBacks.forEach((nextFunction) => {
                      nextFunction(error);
                   });
              }

               this.then = function(resolveCallBack,rejectCallBack){
               if(resolveCallBack){
               this.resolveCallBacks.push(resolveCallBack)
               }
                if(rejectCallBack){
               this.rejectCallBacks.push(rejectCallBack)
               }
               return this;
               }
               executionFunction(resolve, reject);
             } 
