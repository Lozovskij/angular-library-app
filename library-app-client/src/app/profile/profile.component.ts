import { Component } from '@angular/core';
import { Patron, user } from '../temp-data/user';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
  user: Patron | undefined = user;
}
