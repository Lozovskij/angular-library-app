import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BooksCatalogComponent } from './books-catalog/books-catalog.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { CanActivatePatron } from './patron-guard';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  { path: 'catalog', component: BooksCatalogComponent, canActivate:[ CanActivatePatron ] },
  { path: 'books/:bookId', component: BookDetailsComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'login', component: LoginPageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [CanActivatePatron]
})
export class AppRoutingModule { }
