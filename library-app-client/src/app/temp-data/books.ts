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
}

export const books = [
    {
        //https://books.google.by/books/about/War_and_Peace.html?id=s-OQ2yHDIMQC&redir_esc=y
        id: 1,
        title: 'War and Peace, Volume 1',
        author: 'Leo Tolstoy',
        description: `Widely regarded as the greatest novel in 
            any language, War and Peace is primarily concerned with 
            the histories of five aristocratic families--particularly 
            the Bezukhovs, the Bolkonskys, and the Rostovs--the members 
            of which are portrayed against a vivid background of Russian social
            life during the war against Napoleon (1805-14). The theme of war, however,
            is subordinate to the story of family existence, which involves Tolstoy's 
            optimistic belief in the life-asserting pattern of human existence. 
            The heroine, Natasha Rostova, for example, reaches her greatest fulfilment 
            through her marriage to Pierre Bezukhov and her motherhood. The novel also 
            sets forth a theory of history, concluding that there is a minimum of free choice; 
            all is ruled by an inexorable historical determinism`,
    },
    {
        //https://books.google.by/books?id=FmyBAwAAQBAJ&dq=sapiens&source=gbs_navlinks_s
        id: 2,
        title: 'Sapiens: A Brief History of Humankind',
        author: 'Yuval Noah Harari',
        description: `No description`,//description with spacings???
    }
];