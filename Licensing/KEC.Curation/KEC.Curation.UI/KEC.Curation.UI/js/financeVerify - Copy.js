﻿let financeVerifyUrl = apiBaseUrl.concat(`/Publications/process`);

        $(document).ready(function()
        {
            $('#LegalApprove').click(function () {
                $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

                var kicdnumber = $('#kicd').attr('data-kicdNumber');
                var notes = $('.note-editable').html();
                var userGuid = $('#CurrentUserGuid').val();
                var action = "PublicationMoveToNextStage";
                var stages = "PaymentVerification";
               
                
                console.log(`${kicdnumber}`);
                console.log(` ${notes} `);
                console.log(` ${userGuid}`);
                $.ajax({
                    headers :  {
                        'Accept' :  'application/json',
                        'Content-Type' :  'application/json'
                    },
                    url: financeVerifyUrl,
                    type: "PATCH",
                    data: JSON.stringify({ KICDNumber: kicdnumber, Notes: notes, ActionTaken: action, Stage: stages, UserGuid: userGuid }),

                    success: function (response, status, jxhr) {
                        console.log(response);
                        console.log(status);
                        $('#message').html(` ${response}.`)
                        $('div.alert-success').toggleClass('hidden');
                        $('#LegalApprove').html('APPROVE');

                    },
                    error: function (request, status, error) {

                        console.log(request);
                        console.log(status);

                        console.log(request.responseText);
                        $('#error').html(request.responseText)
                        $('div.alert-danger').toggleClass('hidden');
                        $('#LegalApprove').html('APPROVE');


                    }
                });


            });
        },

        );
