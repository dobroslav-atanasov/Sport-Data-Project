import { Component, OnInit } from '@angular/core';
import { IUser } from '../../auth/interfaces/user';
import { AdminsService } from '../services/admins.service';
import { NotificationsService } from '../../shared/services/notifications.service';

@Component({
  selector: 'sd-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit {
  users: IUser[] = [];

  constructor(private adminsService: AdminsService,
    private notificationsService: NotificationsService) { }

  ngOnInit(): void {
    this.adminsService
      .getUsers()
      .subscribe({
        next: (res: IUser[]) => {
          this.users = res.map(x => {
            return <IUser> {
              id: x.id,
              username: x.username
            }
          });
        },
        error: (err) => {
          this.notificationsService.showError(err);
        }
      })
  }
}
