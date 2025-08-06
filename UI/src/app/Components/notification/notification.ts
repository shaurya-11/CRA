import { Component, OnInit } from '@angular/core';
import { Patch } from '../../Models/patch';
import { CustomerPatchQueryDto } from '../../Models/customer-patch-query-dto';
import { PatchNotificationQueryDto } from '../../Models/patch-notification-query';
import { QueryService } from '../../Services/Query/query-service';

@Component({
  selector: 'app-notification',
  standalone: false,
  templateUrl: './notification.html',
  styleUrl: './notification.css'
})
export class NotificationComponent implements OnInit {
  notifications: PatchNotificationQueryDto[] = [];
  selectedNotification: PatchNotificationQueryDto | null = null;
  customerId: number = 0;

  constructor(private queryService: QueryService) {}

  ngOnInit(): void {
    this.customerId = +(localStorage.getItem('customerId') || 0);
    console.log("Customer ID in notification: ",this.customerId);
    if (this.customerId > 0) {
      this.queryService.getPendingPatches(this.customerId).subscribe({
        next: (data) => {
          this.notifications = data;
          console.log("Notifications: ",this.notifications);
          if (this.notifications.length > 0) {
            this.selectedNotification = this.notifications[0];
          }
        },
        error: (error) => {
          console.error('Failed to load pending patches:', error);
        }
      });
    }
  }

  selectNotification(notification: PatchNotificationQueryDto) {
    this.selectedNotification = notification;
  }
  downloadPatch(fileName: string): void {
    console.log("Agent is called for patch: ",fileName)
  }
}
