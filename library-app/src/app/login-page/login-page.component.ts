import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})
export class LoginPageComponent {
  constructor(
    private router: Router,
    private authService: AuthService,
  ) {}
  
  loginPatron(){
    //use auth service
    //redirect on catalog if success
    const sub = this.authService.login('patron').subscribe(
      () => this.router.navigate(['/catalog'])
    );
  }
}
