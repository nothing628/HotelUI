
declare module '@/lib/Test' {
  export function calendarGet(year: number, callback: (items: any) => void): void;
  export function calendarSet(arr_data: Array<any>): void;
  export function execute(sque: any): any;
  export function executeFirst(sque: any): any | undefined;
  export function executeScalar(sque: any): any;
  export function squel(): any;
  export function ss(): any;
  export function su(): any;
  export function sd(): any;
  export function si(): any;
  export function se(): any;
}


