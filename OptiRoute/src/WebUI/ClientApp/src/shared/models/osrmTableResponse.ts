export interface OsrmTableResponse {
  code: string;
  distances: Array<number[]>;
  durations: Array<number[]>;
  sources: Destination[];
  destinations: Destination[];
}

export interface Destination {
  hint: string;
  distance: number;
  location: number[];
  name: string;
}

export interface IDistDur {
  distances: Array<number[]>;
  durations: Array<number[]>;
}
