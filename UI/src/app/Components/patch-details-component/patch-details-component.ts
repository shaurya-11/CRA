import { Component, OnInit } from '@angular/core';
import { PatchDetailsService } from '../../Services/PatchDetails/patch-details';
import { Patch } from '../../Models/patch';
import { QueryService } from '../../Services/Query/query-service';
import { AdminPatchQueryDto } from '../../Models/admin-patch-query-dto';

@Component({
  selector: 'app-patch-details-component',
  standalone: false,
  templateUrl: './patch-details-component.html',
  styleUrl: './patch-details-component.css'
})
export class PatchDetailsComponent implements OnInit {
  role: 'Admin' | 'user' = 'user';
  showForm = false;
  patches: AdminPatchQueryDto[] = [];

  newPatch: Patch = {
  productId: 0,
  fileName: '',
  version: '',
  description: '',
  releasedOn: ''
  };

  constructor(private patchService: PatchDetailsService, private queryService : QueryService) {}

  ngOnInit(): void {
    const storedRole = localStorage.getItem('role');
    this.role = storedRole === 'Admin' ? 'Admin' : 'user';
    
    this.loadPatches();
  }

  loadPatches() {
    this.queryService.getAdminPatchDetails().subscribe({
      next: (data) => {
        this.patches = data;
        console.log(this.patches);
      },
      error: (err) => {
        console.error('Error fetching patches:', err);
      }
    });
  }

  toggleForm() {
    this.showForm = !this.showForm;
  }

  selectedFile: File | null = null;

onFileSelected(event: any) {
  const file = event.target.files[0];
  if (file) {
    this.selectedFile = file;
    console.log("Selected file:", file.name);
  }
}

addPatch() {
  if (!this.selectedFile) {
    alert("Please select a patch file.");
    return;
  }

  const formData = new FormData();
  formData.append("ProductId", this.newPatch.productId.toString());
  formData.append("FileName", this.newPatch.fileName);
  formData.append("Version", this.newPatch.version);
  formData.append("Description", this.newPatch.description);
  formData.append("ReleasedOn", new Date().toISOString());
  formData.append("PatchFile", this.selectedFile); // Important!

  this.patchService.addPatch(formData).subscribe({
    next: (saved) => {
      this.patches.push(saved);
      this.newPatch = {
        productId: 0,
        fileName: '',
        version: '',
        description: '',
        releasedOn: new Date().toISOString()
      };
      this.selectedFile = null;
      this.showForm = false;
    },
    error: (err) => {
      console.error('Error adding patch:', err);
    }
  });
}

  

}