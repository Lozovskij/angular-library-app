export interface Book {
    id: number;
    title: string;
    authors: string[];
    description: string;
    ISBN: string;
    publisher: string;
    yearOfPublication: number;
}

export interface BookInstance {
    id: number;
    title: string;
    status: BookInstanceStatus;
}

export enum BookInstanceStatus {
    Available,
    OnHold,
    CheckedOut,
    Overdue,
}
