import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Customer } from '../../Models/customer';
import { HttpClient } from '@angular/common/http';
import { CustomerDetailsService } from '../../Services/CustomerDetails/customer-details';

@Component({
  selector: 'app-signup-component',
  standalone: false,
  templateUrl: './signup-component.html',
  styleUrl: './signup-component.css'
})
export class SignupComponent implements OnInit {
   @Output() close = new EventEmitter<void>();
  role: 'Admin' | 'user' = 'user';
  showSignup = false;
  customers: Customer[] = [];
  password : string = '';

  newCustomer: Customer = {
    name: '',
    currentPatchVersion: '',
    serverIp: '',
    lastCheckIn: new Date()
  };

  constructor(private customerService: CustomerDetailsService) {}

  ngOnInit(): void {

    this.loadCustomers();
  }
  loadCustomers() {
    this.customerService.getCustomers().subscribe({
      next: (data) => {
        this.customers = data;
        console.log(this.customers);
      },
      error: (err) => {
        console.error('Error fetching customers:', err);
      }
    });
  }

  addCustomer() {
    const newEntry: Customer = {
      ...this.newCustomer,
      lastCheckIn: new Date()
    };
    this.customerService.addCustomer(newEntry).subscribe({
      next: (saved) => {
        this.customers.push(saved);
        this.newCustomer = {
          name: '',
          currentPatchVersion: '',
          serverIp: '',
          lastCheckIn: new Date()
        };
        this.newCustomer = new Customer();
        this.password = '';
        this.showSignup = false;
        this.close.emit();
      },
      error: (err) => {
        console.error('Error adding customer:', err);
      }
    });
  }
}
