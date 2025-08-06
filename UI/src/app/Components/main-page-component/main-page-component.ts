import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QueryService } from '../../Services/Query/query-service';

@Component({
  selector: 'app-main-page-component',
  standalone : false,
  templateUrl: './main-page-component.html',
  styleUrls: ['./main-page-component.css']
})
export class MainPageComponent implements OnInit {
  role: any;
  products: any[] = [];
  dropdownOpen = false;
  customerId: number = 1;
  showProductForm: boolean = false;
    showMenu: boolean = false;
  newProduct = {
    name: '',
    customerId: ''
  };
  constructor(private queryService: QueryService, public router: Router) {}

  ngOnInit(): void {
    this.customerId = +(localStorage.getItem('customerId') || 0);
    this.role = localStorage.getItem('role');
     console.log("Role in main page ",this.role);
      this.queryService.getCustomerProductDetails(this.customerId).subscribe({
        next: (data) => {
          console.log(data);
          this.products = data;
        },
        error: (err) => console.error('Failed to fetch products', err)
      });
  }
  getDotClass(status: string): string {
  const s = status.toLowerCase();
  if (s.includes('pending')) return 'dot-available';
  if (s.includes('downloaded')) return 'dot-updating';
  if (s.includes('installed') || s.includes('up-to-date')) return 'dot-updated';
  return '';
  }
  
  getStatusLabel(status: string): string {
  const s = status?.toLowerCase();
  if (s.includes('installed') || s.includes('up-to-date')) {
    return 'Up to date';
  }
  if (s.includes('pending')) {
    return 'Update Available';
  }
  return status || 'Unknown';
  }

  logout() {
    // Logic to clear user session/token
    console.log('Logged out');
    this.router.navigate(['/login']);
  }

  goToNotifications() {
    this.router.navigate(['home/notifications']);
  }

  toggleMenu() {
    this.showMenu = !this.showMenu;
    console.log("Menu visibility:", this.showMenu);
  }
  closeMenu() {
  this.showMenu = false;
  }
  goToProductDetails(product: any) {
    if (!product || !product.productName) {
    console.error("Product or product.name is undefined", product);
    return;
  }
  console.log("Product",product.productName);
  this.router.navigate(['/home/product-details', product.productName]);
  }

  toggleProductForm(): void {
    this.showProductForm = !this.showProductForm;
    if (!this.showProductForm) {
      this.newProduct = { name: '', customerId: '' };
    }
  }


}
