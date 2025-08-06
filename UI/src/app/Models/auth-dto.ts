export interface AuthDto {
  name: string;
  role: string;
  exp: number;
  // Add any other claims you include in the token
}