import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BooksCatalogComponent } from './books-catalog/books-catalog.component';

const routes: Routes = [
  { path: '', component: BooksCatalogComponent },
  { path: 'books/:bookId', component: BookDetailsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
