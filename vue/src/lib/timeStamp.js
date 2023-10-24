const getTimeStamp = function(){

    let date = new Date;

    let year = date.getUTCFullYear();
    let month = ("00" + date.getUTCMonth()).slice(-2);
    let day = ("00" + date.getUTCDate()).slice(-2);
    let hour = ("00" + date.getHours()).slice(-2);
    let minute = ("00" + date.getMinutes()).slice(-2);

    let timeStamp = year+"-"+month+"-"+day+"T"+hour+":"+minute;

    return timeStamp;

};

const convertTimeStamp = function(input){

  let splitInput = input.split("T");
  let date = splitInput[0].split("-");
  let time = splitInput[1].split(":"); 

  return date[2]+"/"+date[1]+"/"+date[0]+" - "+time[0]+":"+time[1];

};

const removeTimeFromTimeStamp = function(input){

  let splitInput = input.split("T"); 

  return splitInput[0];

};

export {getTimeStamp, convertTimeStamp, removeTimeFromTimeStamp};