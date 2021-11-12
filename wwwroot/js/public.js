//jquery 写法
console.log($("#search"));
$("#search").click(function(){
    let inputName=$(":input").val()
    $.get({
        //url:`http://${window.location.hostname}:44369/Seat/${inputName}`,
        url: `/Seat/${inputName}`,
        success:function(data){
         console.log("10-28"+data);
         let jsonData=JSON.parse(data);
        $(".login").hide();
        $(".end").toggle();
        $("#Name").val(jsonData["name"]);
        $("#Seat").val(jsonData["seat"]);
        },
        error:function(){
            alert("没有查询有效姓名，请检查!")
        }
    })
})
$("#goback").click(function(){
    $(".login").toggle();
    $(".end").hide();
})