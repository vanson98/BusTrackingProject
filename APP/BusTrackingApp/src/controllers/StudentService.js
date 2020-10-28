import httpClient from './httpClient';

const StudentService =  {
    getAllStudentOfMonitor: (monitorId,jwt)=>{
        const url = "/Student/GetByMonitorId?monitorId="+monitorId;
        return httpClient.get(url,{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        });
    },
    getAllChildOfParent: (parentId,jwt)=>{
        const url = "/Student/GetByParentId?parentId="+parentId;
        return httpClient.get(url,{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        });
    },
    checkIn: (studentId,monitorId,stopId,checkInType,checkInTime,checkInResult,jwt)=>{
        const url = "/Student/CheckIn";
        return httpClient.post(url,{
            studentId: studentId,
            monitorId: monitorId,
            stopId: stopId,
            checkInType: checkInType,
            checkInTime: checkInTime,
            checkInResult: checkInResult
        },{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        })
    },
    getAllNotificationOfMonitor: (monitorId,fromDate,toDate,jwt)=>{
        const url = "Student/GetNotificationOfMonitor?monitorId="+monitorId+"&fromDate="+fromDate+"&toDate="+toDate;
        return httpClient.get(url,{
            headers: {
                'Authorization': "Bearer " + jwt
            }
        })
    },
    getAllNotificationOfParent: (parentId,fromDate,toDate,jwt)=>{
        const url = "Student/GetNotificationOfParent?parentId="+parentId+"&fromDate="+fromDate+"&toDate="+toDate;
        console.log(url);
        return httpClient.get(url,{
            headers: {
                'Authorization': "Bearer " + jwt
            }
        })
    }
}

export default StudentService;