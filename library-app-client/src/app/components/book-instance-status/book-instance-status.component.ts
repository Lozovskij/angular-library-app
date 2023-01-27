import { Component, Input } from '@angular/core';
import { BookInstanceStatus } from 'src/app/shared/models/books';

@Component({
  selector: 'app-book-instance-status',
  templateUrl: './book-instance-status.component.html',
  styleUrls: ['./book-instance-status.component.scss']
})
export class BookInstanceStatusComponent {
    @Input()
    public status: BookInstanceStatus | undefined;
}
