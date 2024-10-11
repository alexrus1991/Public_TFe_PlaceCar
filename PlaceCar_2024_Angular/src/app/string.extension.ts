

(String.prototype as any).toHTML = function () {
   return this.replace('\n', '<br>');
 };
  