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
    ) { }

    registerAndLoginPatron() {
        const sub = this.authService.loginRandomDemoPatron().subscribe(
            (token: string) => {
                localStorage.setItem('authToken', token);
                this.router.navigate(['/catalog']);
            }
        );
    }

    loginPatron(id: number) {
        const sub = this.authService.loginDemoPatron(id).subscribe(
            (token: string) => {
                console.log(token);
                localStorage.setItem('authToken', token);
                this.router.navigate(['/catalog']);
            }
        );
    }
}
