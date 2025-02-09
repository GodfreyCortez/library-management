test -f .env && source .env



if [ -z "${RAVEN_License}" ]
then
    echo "Attempting to find license.json at ./ravendb-license"
    LICENSE_JSON="$(cat ./ravendb-license/license.json)"
    RAVEN_License="$(echo $LICENSE_JSON | jq -R)"
else
    echo "Keeping your Raven License"
fi

OPEN_LIBRARY_USERNAME="<Your OPEN LIBRARY username here>"
OPEN_LIBRARY_PASSWORD="<Your OPEN LIBRARY password here>"

ENV_FILE="#################################
# Generated with ${BASH_SOURCE}
#################################

RAVEN_License='${RAVEN_License}'

OPEN_LIBRARY_USERNAME='${OPEN_LIBRARY_USERNAME}'
OPEN_LIBRARY_PASSWORD='${OPEN_LIBRARY_PASSWORD}'
"

echo "${ENV_FILE}" > .env

echo "Done!"