import { Book } from "../types/library/books";

/**
 * Class to interact with the backend of the application
 * to retrieve, create and manage the book library
 */
export class ApiService {
  public static baseUrl = "https://localhost:7024/api";

  /**
   * Retrieves all the books from the application
   */
  public static async getAllBooks(): Promise<Book[]> {
    const resource = "/books";
    const endpoint = this.baseUrl + resource;

    try {
      const response = await fetch(endpoint);
      if (!response.ok) {
        throw new Error(`Response status: ${response.status}`);
      }
      const jsonResult = (await response.json()) as unknown as Book[];

      return jsonResult;
    } catch (e) {
      console.error(
        `[${this.name}]: Unable to retrieve books from ${endpoint}`,
        e
      );
    }

    return [];
  }

  /**
   * Searches for books based off of the query
   */
  public static async searchForBooks(query: string): Promise<Book[]> {
    const resource = "/books/search";
    const params = new URLSearchParams({ query });
    const endpoint = `${this.baseUrl}${resource}?${params.toString()}`;

    try {
      const response = await fetch(endpoint);
      if (!response.ok) {
        throw new Error(`Response status: ${response.status}`);
      }
      const jsonResult = (await response.json()) as unknown as Book[];

      return jsonResult;
    } catch (e) {
      console.error(
        `[${this.name}]: Unable to retrieve books from ${endpoint}`,
        e
      );
    }

    return [];
  }
}
