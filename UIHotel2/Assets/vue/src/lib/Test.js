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

export function execute(sque) {
  let v = sque.toParam();
  let sql = v.text;
  let values = v.values;
  let result = CS.DB.Query(sql, values);
console.log(v);
  return result;
}
export function executeScalar(sque) {
  let v = sque.toParam();
  let sql = v.text;
  let values = v.values;
  let result = CS.DB.QueryScalar(sql, values);
console.log(v);
  return result;
}