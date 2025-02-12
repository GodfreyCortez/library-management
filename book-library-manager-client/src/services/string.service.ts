export class StringService {
  public static CombineStringAsList(list: string[]) {
    const formatter = new Intl.ListFormat("en", {
      style: "long",
      type: "conjunction",
    });

    return formatter.format(list);
  }
}
