import { Customer } from '../Services/CustomerDetails/customer';

describe('Customer', () => {
  it('should create an instance', () => {
    expect(new Customer()).toBeTruthy();
  });
});
