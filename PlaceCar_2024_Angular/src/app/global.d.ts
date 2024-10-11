export {};

declare global {
  interface DateExtension {
    toISO(): String;
  }

  interface StringExtension {
    toHTML(): String;
  }
}
