import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QueryService } from '../../Services/Query/query-service';
import { CustomerPatchQueryDto } from '../../Models/customer-patch-query-dto';
import { AdminPatchQueryDto } from '../../Models/admin-patch-query-dto';

@Component({
  selector: 'app-product-details',
  standalone: false,
  templateUrl: './product-details.html',
  styleUrl: './product-details.css'
})
export class ProductDetailsComponent implements OnInit {
  role: any;
  patchDetails: any;
  productName: string = '';
  product: any;
  customerId : number= 0;
  AdminPatches: AdminPatchQueryDto[] = [];
  constructor(
    private route: ActivatedRoute,
    private queryService: QueryService,
    private http: HttpClient
  ) {}

  ngOnInit() {
  this.role = localStorage.getItem('role');
  this.productName = this.route.snapshot.paramMap.get('name') || '';
  console.log('Product name:', this.productName);
  this.customerId = +(localStorage.getItem('customerId') || 0);

  if (!this.productName) return;
  console.log("Role in product ",this.role);
  if (this.role === 'Admin') {
    this.queryService.getAdminPatchDetails().subscribe({
        next: (data) => {
          // Filter patches for this specific product
          this.AdminPatches = data.filter(p => p.productName === this.productName);
        },
        error: (err) => {
          console.error('Error fetching Admin patches:', err);
        }
      });
  } else {
    this.queryService.getCustomerProductDetails(this.customerId)
      .subscribe(
        (data) => {
          console.log("Data:",data)
          this.patchDetails = data.find((p: CustomerPatchQueryDto)=> p.productName === this.productName);
          if (!this.patchDetails) {
            console.warn('No data found for product:', this.productName);
          }
        },
        (error) => {
          console.error('API error:', error);
        }
      );
  }
  }
  onUpdatePatches(){
    console.log("Update Pateches is called");
  }

}