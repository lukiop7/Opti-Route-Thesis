export function getMinDate(): string {
    let date = new Date();
    date.setHours(0, 0, 0, 0);
    
    const tzoffset = (new Date()).getTimezoneOffset() * 60000; //offset in milliseconds
    return (new Date(date.valueOf() - tzoffset)).toISOString().slice(0, -1);
  }
  