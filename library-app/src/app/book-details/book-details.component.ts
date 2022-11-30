import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book, books } from '../books';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss']
})
export class BookDetailsComponent {
  book: Book | undefined;

  constructor(
    private route: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    // First get the book id from the current route.
    const routeParams = this.route.snapshot.paramMap;
    const bookIdFromRoute = Number(routeParams.get('bookId'));

    // Find the book that correspond with the id provided in route.
    this.book = books.find(b => b.id === bookIdFromRoute);
  }
}
