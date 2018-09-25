/* eslint-disable */
var s = require("squel");

export function squel() {
  return s.useFlavour('mysql');
};
export function ss() {
  return squel().select();
};
export function su() {
  return squel().update();
};
export function sd() {
  return squel().delete();
};
export function si() {
  return squel().insert();
};
export function se() {
  return squel().expr();
};

export function calendarGet(year, callback) {
  CS.DB.CalendarGet(year, callback);
}

export function calendarSet(arr_data) {
  CS.DB.CalendarSet(arr_data);
}

export function execute(sque) {
  let v = sque.toParam();
  let sql = v.text;
  let values = v.values;
  console.log(v);
  let result = CS.DB.Query(sql, values);

  return result;
}
export function executeScalar(sque) {
  let v = sque.toParam();
  let sql = v.text;
  let values = v.values;
  console.log(v);
  let result = CS.DB.QueryScalar(sql, values);

  return result;
}