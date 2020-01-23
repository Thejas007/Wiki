

# Sample grid set up

 ```<ng-container >
                          <ag-grid-angular style="height:600px;" class="ag-theme-balham" [columnDefs]="columnDefs" [floatingFilter]="true"
                            [enableFilter]="true" [enableColResize]="true" [gridOptions]="gridOptions"
                            [rowData]="rowData" [paginationPageSize]="15" [pagination]="true" [enableSorting]="true">
                          </ag-grid-angular>
                        </ng-container>
