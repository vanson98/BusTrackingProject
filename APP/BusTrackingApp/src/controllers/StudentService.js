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
    getAllStudentOfTeacher: (teacherId,jwt)=>{
        const url = "/Student/GetByTeacherId?teacherId="+teacherId;
        return httpClient.get(url,{
            headers:{
                'Authorization': "Bearer " + jwt
            }
        });
    },
    checkIn: (studentId,monitorId,longitude,latitude,checkInType,checkInTime,checkInResult,jwt)=>{
        console.log(jwt);
        const url = "/Student/CheckIn";
        return httpClient.post(url,{
            studentId: studentId,
            monitorId: monitorId,
            longitude: longitude,
            latitude: latitude,
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
        return httpClient.get(url,{
            headers: {
                'Authorization': "Bearer " + jwt
            }
        })
    },

    getAllNotificationOfTeacher: (parentId,fromDate,toDate,jwt)=>{
        const url = "Student/GetNotificationOfTeacher?teacherId="+parentId+"&fromDate="+fromDate+"&toDate="+toDate;
        return httpClient.get(url,{
            headers: {
                'Authorization': "Bearer " + jwt
            }
        })
    },

    getAllStopOfMonitor: (monitorId,typeStop,jwt) =>{
        const url = "Stop/GetAllByMonitor?monitorId="+monitorId+"&typeStop="+typeStop;
        return httpClient.get(url,{
            headers: {
                'Authorization': "Bearer " + jwt
            }
        })
    }
}

export default StudentService;