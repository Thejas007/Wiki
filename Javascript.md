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
            
            serviceCall().then(function(result){
            if(result){
            // do the operation
            }
            });
