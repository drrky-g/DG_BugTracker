//Script for sending table data to ticket details modal

    //listens for "details" button click
    $('.detailFire').click(function () {
            //store the id of the button (has id nested inside of it from [notmapped] model property)
            //the button id looks like this: 't{Ticket.Id}btn'
            var btnFullId = this.id;
        //split the characters of the id string into an array
        var idBreak = btnFullId.split('')
        //removes 'btn' from end of id
        idBreak.splice(idBreak.length - 3, 3);
        //take 't' from beginning of string
        idBreak.splice(0, 1);
        //remaining values in 'idBreak' string is now solely the id of the ticket in the model
        var targetCore = idBreak.join('');

        //rebuild the targetting strings out of the remaining value (the id) (these are [notmapped] model properties)
        var idCellTarget = "t" + targetCore + "Id";
        var titleCellTarget = "t" + targetCore + "Title";
        var descrCellTarget = "t" + targetCore + "Desc"; //done
        var createdCellTarget = "t" + targetCore + "Created"; //done
        var updatedCellTarget = "t" + targetCore + "Updated"; //done
        var projectCellTarget = "t" + targetCore + "Project";
        var typeCellTarget = "t" + targetCore + "Type"; //done
        var statusCellTarget = "t" + targetCore + "Status"; //done
        var priorCellTarget = "t" + targetCore + "Priority"; //done
        var subCellTarget = "t" + targetCore + "Sub"; //done
        var devCellTarget = "t" + targetCore + "Dev"; //done
        var commContainer = "t" + targetCore + "Comms"; //done
        var attachContainer = "t" + targetCore + "Attach"; //done
        var historyContainer = "t" + targetCore + "Hist"; //done
        var devPicSrc = "t" + targetCore + "devpic";
        var subPicSrc = "t" + targetCore + "subpic";


        //pull data inside the cells into variables
        var idToDetails = document.getElementById(idCellTarget).innerText; //format with title
        var titleToDetails = document.getElementById(titleCellTarget).innerText; //format with id
        //formatting header info to html
                var headerOutput = titleToDetails + '<br /><small class="text-muted">(Ticket Id: ' + idToDetails + ')</small>';

        var descToDetails = document.getElementById(descrCellTarget).innerText; //done
        var createdToDetails = document.getElementById(createdCellTarget).innerText; //done
        var updatedToDetails = document.getElementById(updatedCellTarget).innerText; //done
        var projToDetails = document.getElementById(projectCellTarget).innerText; //done
        // takes variable and formats it how i want it in the modal target element
        var projOutput = "Project: " + projToDetails;


        //pictures
        var subAvatarSrc = document.getElementById(subPicSrc).innerText;
        var devAvatarSrc = document.getElementById(devPicSrc).innerText;

        var typeToDetails = document.getElementById(typeCellTarget).innerText; //done
        var statusToDetails = document.getElementById(statusCellTarget).innerText; //done
        var priorToDetails = document.getElementById(priorCellTarget).innerText; //done
        var subToDetails = document.getElementById(subCellTarget).innerText; //done
        var devToDetails = document.getElementById(devCellTarget).innerText; //done
        var commentsToDetails = document.getElementById(commContainer).innerHTML; //done
        var attachmentsToDetails = document.getElementById(attachContainer).innerHTML; //done
        var historiesToDetails = document.getElementById(historyContainer).innerHTML;

        //push that data back into the modal targeted elements
        $('#headerTarget').html(headerOutput);
        $('#idPropTarget').text(idToDetails);
        $('#titlePropTarget').text(titleToDetails);
        $('#descPropTarget').text(descToDetails);
        $('#crePropTarget').text(createdToDetails);
        $('#updPropTarget').text(updatedToDetails);
        $('#projPropTarget').text(projOutput);
        $('#typePropTarget').text(typeToDetails);
        $('#statusPropTarget').text(statusToDetails);
        $('#priorityPropTarget').text(priorToDetails);
        $('#subPropTarget').text(subToDetails);
        $('#devPropTarget').text(devToDetails);
        $('#commentCollTarget').html(commentsToDetails);
        $('#attachCollTarget').html(attachmentsToDetails);
        $('#histCollTarget').html(historiesToDetails);

        //image targets
        $('#devDetailImg').attr("src", devAvatarSrc);
        $('#subDetailImg').attr("src", subAvatarSrc);

        //hidden values for forms get passed
        $('#attachmentTicketId').attr("value", targetCore);
        $('#commentTicketId').attr("value", targetCore);


    });