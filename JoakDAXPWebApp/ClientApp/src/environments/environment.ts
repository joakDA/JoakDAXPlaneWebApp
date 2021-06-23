// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: 'https://localhost:44395',
  signalRHub: 'https://localhost:44395/XPlaneHub/',
  initialMapLatitude: 40.4844168,
  initialMapLongitude: -3.6927541,
  initialMapZoom: 11,
  receptionTimeoutSignalR: 10000 // In milliseconds
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
