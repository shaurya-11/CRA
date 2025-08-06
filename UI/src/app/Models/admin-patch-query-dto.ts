export interface AdminPatchQueryDto {
  productName: string;
  patchName: string;
  version: string;
  description: string;
  releaseDate: string; // or Date
  customersUpdatedCount: number;
}