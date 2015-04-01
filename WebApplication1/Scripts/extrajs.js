$( document ).ready(function() {	
	//$('#body').fadeIn(300);
	setTimeout(function(){
    $("#alert").slideUp("slow"); }
	,3000)
});

$('#dismissBtn').click(function () {
    //3000 is a time duration(in ms) for transition
	$('#alert').slideUp(500);
    $('#alert').fadeOut(50);
    return false;
}); 

