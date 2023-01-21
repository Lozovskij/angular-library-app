import { Component } from '@angular/core';
import { Patron, user } from '../temp-data/user';
import { PatronProfile, ProfileService } from './profile.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent {
    profile: PatronProfile | null = null;
    constructor(profileService: ProfileService) {
        profileService.getPatronProfile().subscribe(p => {
            console.log(p);
            this.profile = p;
        });
    }
}
