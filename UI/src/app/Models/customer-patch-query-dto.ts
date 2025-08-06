export interface CustomerPatchQueryDto {
  customerName : string;
  productName: string;
  customerPatchVersion: string;
  latestPatchVersion: string;
  latestPatchDescription: string;
  latestPatchReleaseDate: string; // or Date
  patchStatus: number;            // if it's an enum
  statusText: string;
}
