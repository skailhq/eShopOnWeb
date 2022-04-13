#!/bin/bash
podman run --rm -it \
	-e ASPNETCORE_ENVIRONMENT="Development" \
	-e SIDECAR="dev-nomad-01.skail.me:5003" \
	-e SkailPlatform__Logging__LogLevel="Default" \
	-e NAMESPACE="eShopOnWeb" \
	-e IMAGE="oci://registry.digitalocean.com/skail/func/sendemail:v0.0.1" \
	-e BATCH_SIZE="1" \
	-e SMTP_SERVER="127.0.0.1:2525" \
	registry.digitalocean.com/skail/func/sendemail:v0.0.1-skail
