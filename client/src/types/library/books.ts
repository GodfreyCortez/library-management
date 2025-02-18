export type Book = {
  id: string;
  title: string | null;
  authors: string[] | null;
  authorKeys: string[] | null;
  coverId: string | null;
  firstPublishYear: string | null;
  description: string | null;
};
