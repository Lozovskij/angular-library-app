import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './pages/book-details/book-details.component';
import { BooksCatalogComponent } from './pages/books-catalog/books-catalog.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { CanActivatePatron } from './patron-guard';
import { ProfileComponent } from './pages/profile/profile.component';

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
