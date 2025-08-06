import { Component } from '@angular/core';
import { CustomerDetailsService } from '../../Services/CustomerDetails/customer-details'
import { Customer } from '../../Models/customer';
import { QueryService } from '../../Services/Query/query-service';
import { CustomerPatchQueryDto } from '../../Models/customer-patch-query-dto';

@Component({
  selector: 'app-customer-details-component',
  standalone: false,
  templateUrl: './customer-details-component.html',
  styleUrl: './customer-details-component.css'
})
export class CustomerDetailsComponent {
  customerDetails: CustomerPatchQueryDto[] = []; 
  customerId : string = '';
  constructor(private queryService: QueryService) {}

  ngOnInit(): void {
    this.customerId = (localStorage.getItem('customerId') || '0');
    console.log(this.customerId);
    this.queryService.getCustomerProductDetails(+(this.customerId))
          .subscribe(
            (data) => {
              console.log("Data:",data)
              this.customerDetails = data;
              if (!this.customerDetails) {
                console.warn('No data found for customer');
              }
            },
            (error) => {
              console.error('API error:', error);
            }
          );
  }
}


