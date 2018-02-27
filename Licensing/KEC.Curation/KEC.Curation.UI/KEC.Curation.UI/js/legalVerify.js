
let legalVerifyUrl = apiBaseUrl.concat(`/Publications/process`);

        $(document).ready(function () {
            $('#LegalApprove').click(function () {
                $('#LegalApprove').html('<i class="fa fa-refresh fa-spin"></i> Please wait');

                var kicdnumber = $('#kicd').attr('data-kicdNumber');
                var notes = $('.note-editable').val();
                var action = $('#actionTaken').val();
                var stages = "LegalVerification";
                var guid = $('#guid').attr('data-guid');
                console.log(`${kicdnumber}`);
                console.log(` ${notes} `);
                console.log(` ${guid} `);
                $.ajax({
                    headers : {
                        'Accept' : 'application/json',
                        'Content-Type' : 'application/json'
                    },
                    url: legalVerifyUrl,
                    type: "PATCH",
                    data: JSON.stringify({ KICDNumber: kicdnumber, Notes: notes, ActionTaken: action, Stage: stages, UserGuid: guid }),

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
                        $('#error').html(responseText)
                        $('div.alert-danger').toggleClass('hidden');
                        $('#LegalApprove').html('APPROVE');


                    }
                });


            });
        },

    );

