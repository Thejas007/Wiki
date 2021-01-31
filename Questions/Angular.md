# Angular

1. ### Angular architecture (Building blocks)

   ![image](https://angular.io/generated/images/guide/architecture/overview2.png)
   ![diagram2](https://cdn.educba.com/academy/wp-content/uploads/2019/12/angular-2-architecture.png)
  

1. ### life cycle hooks
    ![Life cycle hooks](https://codecraft.tv/assets/images/courses/angular/4.components/lifecycle-hooks.png)
    
    constructor
    This is invoked when Angular creates a component or directive by calling new on the class.

    ngOnChanges
    Invoked every time there is a change in one of th input properties of the component.

    ngOnInit
    Invoked when given component has been initialized.
    This hook is only called once after the first ngOnChanges

    ngDoCheck
    Invoked when the change detector of the given component is invoked. It allows us to implement our own change detection algorithm for the given component.

    Important
    ngDoCheck and ngOnChanges should not be implemented together on the same component.
    
    ngOnDestroy
    This method will be invoked just before Angular destroys the component.
    Use this hook to unsubscribe observables and detach event handlers to avoid memory leaks.

    Hooks for the Component’s Children
    These hooks are only called for components and not directives.

    ngAfterContentInit
    Invoked after Angular performs any content projection into the component’s view (see the previous lecture on Content Projection for more info).

    ngAfterContentChecked
    Invoked each time the content of the given component has been checked by the change detection mechanism of Angular.

    ngAfterViewInit
    Invoked when the component’s view has been fully initialized.

    ngAfterViewChecked
    Invoked each time the view of the given component has been checked by the change detection mechanism of Angular.


1. ### Lazy loading a module

   Lazy loading is a module which is used to decrease the start-up time. 
   When lazy is used, then our system application does not need to load everything at once.
   It only needs to load what the user expects to see when the application first loads. 
   The modules which are lazily loaded will only be loaded when the user navigates to their routes. Lazy loading improves the performance of our system applications. 
   It keeps the initial payload small and these smaller payloads lead to faster download speeds. 
   It helps in lowering the resource cost, especially on mobile networks. 
   If a user doesn’t visit a section of the application, they won’t ever download those resources. 
   The concept of lazy loading in angular requires us to format the application in a certain way.
   All the assets that are to be lazy loaded should be added to its own module. 
   Lazy loading is setup in the main routing file. 
   Lazy loading overcomes the problem of slow loading of applications in their own way which hence improves the loading time of the application.
   
   https://angular.io/guide/lazy-loading-ngmodules
   
    ```javascript
       const routes: Routes = [
      {
        path: 'customers',
        loadChildren: () => import('./customers/customers.module').then(m => m.CustomersModule)
      },
      {
        path: 'orders',
        loadChildren: () => import('./orders/orders.module').then(m => m.OrdersModule)
      }
    ];
    ```

1. ### Communications between components

   - Using services
   - @Input and @Output 
      ```javascript
         class JokeComponent {
        @Input('joke') data: Joke;
      }
      
      <joke *ngFor="let j of jokes" [joke]="j"></joke>
      ```
      ```javascript
            class JokeFormComponent {
        @Output() jokeCreated = new EventEmitter<Joke>();

        createJoke() {
          this.jokeCreated.emit(new Joke("A setup", "A punchline"));
        }
      }
      
      <joke-form (jokeCreated)="addJoke($event)"></joke-form>
      
      ```
1. ### Model driven forms with validation

      ```javascript
      import { FormGroup, FormControl, Validators } from '@angular/forms';
      .
      .
      .
      class ModelFormComponent implements OnInit {
        myform: FormGroup;

        ngOnInit() {
          myform = new FormGroup({
              name: new FormGroup({
                  firstName: new FormControl('', Validators.required), (1)
                  lastName: new FormControl('', Validators.required),
              }),
              email: new FormControl('', [ (2)
                  Validators.required,
                  Validators.pattern("[^ @]*@[^ @]*") (3)
              ]),
              password: new FormControl('', [
                  Validators.minLength(8), (4)
                  Validators.required
              ]),
              language: new FormControl() (5)
          });
        }
      }
      ```
     
